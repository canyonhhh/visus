class AttendanceRecordsController < ApplicationController
  before_action :set_attendance_record, only: %i[ show edit update destroy ]

  def index
    @attendance_records = AttendanceRecord.all
  end

  def show
  end

  def new
    @attendance_record = AttendanceRecord.new
  end

  def edit
  end

  def create
    @attendance_record = AttendanceRecord.new(attendance_record_params)

    if @attendance_record.save
      redirect_to @attendance_record, notice: "Attendance record was successfully created."
    else
      render :new, status: :unprocessable_entity
    end
  end

  def update
    if @attendance_record.update(attendance_record_params)
      redirect_to @attendance_record, notice: "Attendance record was successfully updated.", status: :see_other
    else
      render :edit, status: :unprocessable_entity
    end
  end

  def destroy
    @attendance_record.destroy!

    redirect_to attendance_records_path, notice: "Attendance record was successfully destroyed.", status: :see_other
  end

  private

  def set_attendance_record
    @attendance_record = AttendanceRecord.find(params.expect(:id))
  end

  def attendance_record_params
    params.expect(attendance_record: [ :activity_id, :student_id ])
  end
end
